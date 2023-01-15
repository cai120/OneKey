var onekey = function (){
    this.name = 'OneKey';
}

$.onekey = new onekey;
$.onekey.Constructor = onekey;

onekey.prototype.init = function(){
    console.log('jQuery inited');
}

onekey.prototype.passwordIndex = function(filterUrl)
{
    debugger;
    var table = $('#password').DataTable({
        "ajax": {
            "type": "GET",
            "url": filterUrl,
            success: function (result) {
                //debugger;
            }
        },
        "columns": [
            { "data": "detailsFor" },
            { "data": "website" }
        ]

    });
}