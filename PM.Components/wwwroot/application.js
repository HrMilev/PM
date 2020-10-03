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