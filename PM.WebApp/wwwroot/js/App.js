function setBlazorCulture(value) {
    localStorage.setItem('BlazorCulture', value);
}

function getBlazorCulture() {
    return localStorage.getItem('BlazorCulture');
}

function browserHistoryBack() {
    history.back();
}