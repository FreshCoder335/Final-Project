using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preciousportfolio.Data;
using preciousportfolio.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Security.Claims;
using System.Text;

namespace preciousportfolio.Controllers
{
    [Authorize] // Only logged-in users can access dashboard features
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the main dashboard using live database data.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Load this user's holdings, newest first
            var userHoldings = await _context.Holdings
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.Id)
                .ToListAsync();

            // Build grouped summary rows by metal
            var holdingsRows = userHoldings
                .GroupBy(h => h.MetalType)
                .Select(g => new HoldingsRow
                {
                    Metal = g.Key,
                    Ounces = g.Sum(x => x.WeightOz * x.Quantity),
                    AvgCostUsd = g.Where(x => x.AcquiredPrice.HasValue).Any()
                        ? g.Where(x => x.AcquiredPrice.HasValue).Average(x => x.AcquiredPrice ?? 0m)
                        : 0m,
                    CurrentPriceUsd = 0m
                })
                .OrderBy(h => h.Metal)
                .ToList();

            // Show up to 5 recent holdings on the preview card
            var recentHoldings = userHoldings
                .Take(5)
                .Select(h => new RecentHoldingItem
                {
                    Id = h.Id,
                    MetalType = h.MetalType,
                    FormType = h.FormType,
                    Description = h.Description,
                    WeightOz = h.WeightOz,
                    Quantity = h.Quantity,
                    Purity = h.Purity
                })
                .ToList();

            var vm = new DashboardViewModel
            {
                DisplayName = User.Identity?.Name ?? "User",
                TotalValueUsd = userHoldings.Sum(h => (h.AcquiredPrice ?? 0m) * h.Quantity),
                TotalHoldingsCount = userHoldings.Count,
                GoldOz = userHoldings
                    .Where(h => h.MetalType == "Gold")
                    .Sum(h => h.WeightOz * h.Quantity),
                SilverOz = userHoldings
                    .Where(h => h.MetalType == "Silver")
                    .Sum(h => h.WeightOz * h.Quantity),
                LastUpdated = DateTime.Now,
                Holdings = holdingsRows,
                RecentHoldings = recentHoldings
            };

            return View(vm);
        }

        /// <summary>
        /// Displays the full holdings list for the current user.
        /// </summary>
        public async Task<IActionResult> Holdings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holdings = await _context.Holdings
                .Where(h => h.UserId == userId)
                .OrderBy(h => h.MetalType)
                .ThenBy(h => h.Description)
                .ToListAsync();

            return View(holdings);
        }

        /// <summary>
        /// Displays the add holding form.
        /// </summary>
        [HttpGet]
        public IActionResult AddHoldings()
        {
            return View(new Holding());
        }

        /// <summary>
        /// Saves a new holding for the current user.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHoldings(Holding holding)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return Challenge();
            }

            // Assign the holding to the logged-in user
            holding.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Prevent validation issues from navigation properties
            ModelState.Remove(nameof(Holding.UserId));
            ModelState.Remove(nameof(Holding.User));

            if (!ModelState.IsValid)
            {
                return View(holding);
            }

            _context.Holdings.Add(holding);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding saved successfully.";
            return RedirectToAction(nameof(Holdings));
        }

        /// <summary>
        /// Displays the edit form for a holding.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> EditHolding(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (holding == null)
            {
                return NotFound();
            }

            return View(holding);
        }

        /// <summary>
        /// Updates an existing holding.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHolding(int id, Holding holding)
        {
            if (id != holding.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingHolding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (existingHolding == null)
            {
                return NotFound();
            }

            holding.UserId = userId;

            ModelState.Remove(nameof(Holding.UserId));
            ModelState.Remove(nameof(Holding.User));

            if (!ModelState.IsValid)
            {
                return View(holding);
            }

            // Update editable fields only
            existingHolding.MetalType = holding.MetalType;
            existingHolding.FormType = holding.FormType;
            existingHolding.Description = holding.Description;
            existingHolding.WeightOz = holding.WeightOz;
            existingHolding.Purity = holding.Purity;
            existingHolding.Quantity = holding.Quantity;
            existingHolding.AcquiredDate = holding.AcquiredDate;
            existingHolding.AcquiredPrice = holding.AcquiredPrice;
            existingHolding.Dealer = holding.Dealer;
            existingHolding.StorageLocation = holding.StorageLocation;
            existingHolding.Notes = holding.Notes;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding updated successfully.";
            return RedirectToAction(nameof(Holdings));
        }

        /// <summary>
        /// Deletes a holding owned by the current user.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHolding(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (holding == null)
            {
                return NotFound();
            }

            _context.Holdings.Remove(holding);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding deleted successfully.";
            return RedirectToAction(nameof(Holdings));
        }

        /// <summary>
        /// Displays the sell form for a holding.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SellHolding(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

            if (holding == null)
            {
                return NotFound();
            }

            var vm = new SellHoldingViewModel
            {
                HoldingId = holding.Id,
                MetalType = holding.MetalType,
                Description = holding.Description,
                WeightOz = holding.WeightOz,
                AvailableQuantity = holding.Quantity,
                AcquiredPrice = holding.AcquiredPrice,
                QuantitySold = 1,
                DateSold = DateTime.Today
            };

            return View(vm);
        }

        /// <summary>
        /// Creates a sale transaction and updates the holding quantity.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellHolding(SellHoldingViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holding = await _context.Holdings
                .FirstOrDefaultAsync(h => h.Id == model.HoldingId && h.UserId == userId);

            if (holding == null)
            {
                return NotFound();
            }

            // Validate quantity sold
            if (model.QuantitySold < 1 || model.QuantitySold > holding.Quantity)
            {
                ModelState.AddModelError(nameof(model.QuantitySold),
                    "Quantity sold must be between 1 and the available quantity.");
            }

            if (!ModelState.IsValid)
            {
                // Refill readonly fields before returning the view
                model.MetalType = holding.MetalType;
                model.Description = holding.Description;
                model.WeightOz = holding.WeightOz;
                model.AvailableQuantity = holding.Quantity;
                model.AcquiredPrice = holding.AcquiredPrice;

                return View(model);
            }

            // Calculate cost basis for the quantity sold
            var unitCost = holding.AcquiredPrice ?? 0m;
            var costBasis = unitCost * model.QuantitySold;

            var sale = new SaleTransaction
            {
                HoldingId = holding.Id,
                MetalType = holding.MetalType,
                Description = holding.Description,
                Quantity = model.QuantitySold,

                // Store total weight sold for the transaction
                WeightOz = holding.WeightOz * model.QuantitySold,

                DateSold = model.DateSold,
                Proceeds = model.Proceeds,
                CostBasis = costBasis,
                UserId = userId
            };

            _context.SaleTransactions.Add(sale);

            // Remove the holding if fully sold, otherwise reduce quantity
            if (model.QuantitySold == holding.Quantity)
            {
                _context.Holdings.Remove(holding);
            }
            else
            {
                holding.Quantity -= model.QuantitySold;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Holding sold successfully.";
            return RedirectToAction(nameof(SalesReport));
        }

        /// <summary>
        /// Displays the inventory report page.
        /// </summary>
        public async Task<IActionResult> InventoryReport()
        {
            var vm = await BuildInventoryReportViewModelAsync();
            return View(vm);
        }

        /// <summary>
        /// Exports the inventory report as CSV.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExportInventoryCsv()
        {
            var vm = await BuildInventoryReportViewModelAsync();

            using var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("Metal");
                csv.WriteField("Description");
                csv.WriteField("Weight (oz)");
                csv.WriteField("Purity");
                csv.WriteField("Quantity");
                csv.WriteField("Storage Location");
                csv.WriteField("Acquired Price");
                csv.WriteField("Cost Basis");
                csv.NextRecord();

                foreach (var row in vm.Rows)
                {
                    csv.WriteField(row.MetalType);
                    csv.WriteField(row.Description);
                    csv.WriteField(row.WeightOz);
                    csv.WriteField(row.Purity);
                    csv.WriteField(row.Quantity);
                    csv.WriteField(row.StorageLocation);
                    csv.WriteField(row.AcquiredPrice ?? 0m);
                    csv.WriteField(row.CostBasis);
                    csv.NextRecord();
                }

                writer.Flush();
            }

            memoryStream.Position = 0;

            return File(
                memoryStream.ToArray(),
                "text/csv",
                $"inventory-report-{DateTime.Now:yyyy-MM-dd}.csv");
        }

        /// <summary>
        /// Exports the inventory report as PDF.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExportInventoryPdf()
        {
            var vm = await BuildInventoryReportViewModelAsync();

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Inventory Report")
                        .SemiBold()
                        .FontSize(20);

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Generated: {DateTime.Now:g}");
                        column.Item().Text($"Total Holdings: {vm.TotalHoldingsValue:C}");
                        column.Item().Text($"Total Weight (oz): {vm.TotalWeightOz:0.####}");
                        column.Item().Text($"Estimated Value: {vm.EstimatedValue:C}");

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Metal").SemiBold();
                                header.Cell().Text("Description").SemiBold();
                                header.Cell().AlignRight().Text("Weight").SemiBold();
                                header.Cell().AlignRight().Text("Purity").SemiBold();
                                header.Cell().AlignRight().Text("Qty").SemiBold();
                                header.Cell().Text("Storage").SemiBold();
                            });

                            foreach (var row in vm.Rows)
                            {
                                table.Cell().Text(row.MetalType);
                                table.Cell().Text(row.Description);
                                table.Cell().AlignRight().Text(row.WeightOz.ToString("0.####"));
                                table.Cell().AlignRight().Text(row.Purity);
                                table.Cell().AlignRight().Text(row.Quantity.ToString());
                                table.Cell().Text(row.StorageLocation);
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("PreciousPortfolio Inventory Report");
                });
            }).GeneratePdf();

            return File(
                pdfBytes,
                "application/pdf",
                $"inventory-report-{DateTime.Now:yyyy-MM-dd}.pdf");
        }

        /// <summary>
        /// Builds the inventory report model for page display and exports.
        /// </summary>
        private async Task<InventoryReportViewModel> BuildInventoryReportViewModelAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var holdings = await _context.Holdings
                .Where(h => h.UserId == userId)
                .OrderBy(h => h.MetalType)
                .ThenBy(h => h.Description)
                .ToListAsync();

            var rows = holdings.Select(h => new InventoryReportRow
            {
                Id = h.Id,
                MetalType = h.MetalType,
                Description = h.Description,
                WeightOz = h.WeightOz,
                Purity = h.Purity,
                Quantity = h.Quantity,
                StorageLocation = h.StorageLocation ?? "",
                TotalOunces = h.WeightOz * h.Quantity,
                AcquiredPrice = h.AcquiredPrice,
                CostBasis = (h.AcquiredPrice ?? 0m) * h.Quantity
            }).ToList();

            return new InventoryReportViewModel
            {
                TotalHoldingsValue = rows.Sum(r => r.CostBasis),
                TotalWeightOz = rows.Sum(r => r.TotalOunces),
                EstimatedValue = rows.Sum(r => r.CostBasis), // Uses entered price data
                Rows = rows
            };
        }

        /// <summary>
        /// Displays the sales report page with optional filters.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SalesReport(string? selectedMetalType, DateTime? startDate, DateTime? endDate)
        {
            var vm = await BuildSalesReportViewModelAsync(selectedMetalType, startDate, endDate);
            return View(vm);
        }

        /// <summary>
        /// Exports the sales report as CSV.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExportSalesCsv(string? selectedMetalType, DateTime? startDate, DateTime? endDate)
        {
            var vm = await BuildSalesReportViewModelAsync(selectedMetalType, startDate, endDate);

            using var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("Date Sold");
                csv.WriteField("Metal");
                csv.WriteField("Description");
                csv.WriteField("Quantity");
                csv.WriteField("Weight (oz)");
                csv.WriteField("Proceeds");
                csv.WriteField("Cost Basis");
                csv.WriteField("Gain/Loss");
                csv.NextRecord();

                foreach (var row in vm.Rows)
                {
                    csv.WriteField(row.DateSold.ToString("yyyy-MM-dd"));
                    csv.WriteField(row.MetalType);
                    csv.WriteField(row.Description);
                    csv.WriteField(row.Quantity);
                    csv.WriteField(row.WeightOz);
                    csv.WriteField(row.Proceeds);
                    csv.WriteField(row.CostBasis);
                    csv.WriteField(row.GainLoss);
                    csv.NextRecord();
                }

                writer.Flush();
            }

            memoryStream.Position = 0;

            return File(
                memoryStream.ToArray(),
                "text/csv",
                $"sales-report-{DateTime.Now:yyyy-MM-dd}.csv");
        }

        /// <summary>
        /// Exports the sales report as PDF.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExportSalesPdf(string? selectedMetalType, DateTime? startDate, DateTime? endDate)
        {
            var vm = await BuildSalesReportViewModelAsync(selectedMetalType, startDate, endDate);

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Sales Report")
                        .SemiBold()
                        .FontSize(20);

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Generated: {DateTime.Now:g}");
                        column.Item().Text($"Total Sales: {vm.TotalSalesCount}");
                        column.Item().Text($"Total Proceeds: {vm.TotalProceeds:C}");
                        column.Item().Text($"Realized Gain/Loss: {vm.TotalGainLoss:C}");

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1.5f); // Date
                                columns.RelativeColumn(1);    // Metal
                                columns.RelativeColumn(2);    // Description
                                columns.RelativeColumn(0.8f); // Qty
                                columns.RelativeColumn(1);    // Weight
                                columns.RelativeColumn(1.2f); // Proceeds
                                columns.RelativeColumn(1.2f); // Cost Basis
                                columns.RelativeColumn(1.2f); // Gain/Loss
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Date Sold").SemiBold();
                                header.Cell().Text("Metal").SemiBold();
                                header.Cell().Text("Description").SemiBold();
                                header.Cell().AlignRight().Text("Qty").SemiBold();
                                header.Cell().AlignRight().Text("Weight").SemiBold();
                                header.Cell().AlignRight().Text("Proceeds").SemiBold();
                                header.Cell().AlignRight().Text("Cost Basis").SemiBold();
                                header.Cell().AlignRight().Text("Gain/Loss").SemiBold();
                            });

                            foreach (var row in vm.Rows)
                            {
                                table.Cell().Text(row.DateSold.ToString("yyyy-MM-dd"));
                                table.Cell().Text(row.MetalType);
                                table.Cell().Text(row.Description);
                                table.Cell().AlignRight().Text(row.Quantity.ToString());
                                table.Cell().AlignRight().Text(row.WeightOz.ToString("0.####"));
                                table.Cell().AlignRight().Text(row.Proceeds.ToString("C"));
                                table.Cell().AlignRight().Text(row.CostBasis.ToString("C"));
                                table.Cell().AlignRight().Text(row.GainLoss.ToString("C"));
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("PreciousPortfolio Sales Report");
                });
            }).GeneratePdf();

            return File(
                pdfBytes,
                "application/pdf",
                $"sales-report-{DateTime.Now:yyyy-MM-dd}.pdf");
        }

        /// <summary>
        /// Builds the sales report model for page display and exports.
        /// </summary>
        private async Task<SalesReportViewModel> BuildSalesReportViewModelAsync(
            string? selectedMetalType,
            DateTime? startDate,
            DateTime? endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Start with this user's sale transactions
            var query = _context.SaleTransactions
                .Where(s => s.UserId == userId);

            // Apply metal filter
            if (!string.IsNullOrWhiteSpace(selectedMetalType))
            {
                query = query.Where(s => s.MetalType == selectedMetalType);
            }

            // Apply start date filter
            if (startDate.HasValue)
            {
                query = query.Where(s => s.DateSold >= startDate.Value);
            }

            // Apply end date filter
            if (endDate.HasValue)
            {
                query = query.Where(s => s.DateSold <= endDate.Value);
            }

            // Load filtered sales, newest first
            var sales = await query
                .OrderByDescending(s => s.DateSold)
                .ThenByDescending(s => s.Id)
                .ToListAsync();

            var rows = sales.Select(s => new SalesReportRow
            {
                Id = s.Id,
                DateSold = s.DateSold,
                MetalType = s.MetalType,
                Description = s.Description,
                Quantity = s.Quantity,
                WeightOz = s.WeightOz,
                Proceeds = s.Proceeds,
                CostBasis = s.CostBasis,
                GainLoss = s.Proceeds - s.CostBasis
            }).ToList();

            // Load available metal types for filter dropdown
            var metalTypes = await _context.SaleTransactions
                .Where(s => s.UserId == userId)
                .Select(s => s.MetalType)
                .Distinct()
                .OrderBy(m => m)
                .ToListAsync();

            return new SalesReportViewModel
            {
                SelectedMetalType = selectedMetalType ?? "",
                StartDate = startDate,
                EndDate = endDate,
                MetalTypes = metalTypes,
                TotalSalesCount = rows.Count,
                TotalProceeds = rows.Sum(r => r.Proceeds),
                TotalGainLoss = rows.Sum(r => r.GainLoss),
                Rows = rows
            };
        }

        /// <summary>
        /// Displays the reports landing page.
        /// </summary>
        public IActionResult Reports()
        {
            return View();
        }

        /// <summary>
        /// Displays the account settings page.
        /// </summary>
        public IActionResult Account()
        {
            return View();
        }

        /// <summary>
        /// Handles the placeholder account form submission.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Account(string displayName, string email, string storageDefault)
        {
            TempData["AccountMessage"] = "Account changes saved (placeholder).";
            return RedirectToAction("Account");
        }

        /// <summary>
        /// Displays the application settings page.
        /// </summary>
        public IActionResult Settings()
        {
            return View();
        }
    }
}