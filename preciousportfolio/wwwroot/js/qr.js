window.addEventListener('load', function () {
    var qrCodeElement = document.getElementById('qrCode');
    var qrCodeDataElement = document.getElementById('qrCodeData');

    if (!qrCodeElement || !qrCodeDataElement) {
        return;
    }

    var qrCodeData = qrCodeDataElement.getAttribute('data-url');

    if (!qrCodeData) {
        return;
    }

    new QRCode(qrCodeElement, {
        text: qrCodeData,
        width: 150,
        height: 150
    });
});