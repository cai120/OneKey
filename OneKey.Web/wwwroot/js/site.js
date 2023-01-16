var onekey = function () {
    this.name = 'OneKey';
}

$.onekey = new onekey;
$.onekey.Constructor = onekey;

onekey.prototype.init = function () {
    console.log('jQuery inited');
}