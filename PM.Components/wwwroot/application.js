function setBlazorCulture(value) {
    document.cookie = `BlazorCulture=${value}; Path=/; expires=${new Date(2147483647 * 1000).toUTCString()}`;
}

function getBlazorCulture() {
    return document.cookie.split('; ').reduce((a, x) => {
        var t = x.split('=');
        a[t[0]] = t[1];
        return a;
    }, {})['BlazorCulture'];
}

function browserHistoryBack() {
    history.back();
}

function downloadFromUrl({ url, fileName }) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

function downloadFromByteArray({
    byteArray,
    fileName,
    contentType
}) {
    const numArray = atob(byteArray).split('').map(c => c.charCodeAt(0));
    const uint8Array = new Uint8Array(numArray);
    const blob = new Blob([uint8Array], { type: contentType });
    const url = URL.createObjectURL(blob);
    downloadFromUrl({ url: url, fileName: fileName });
    URL.revokeObjectURL(url);
}