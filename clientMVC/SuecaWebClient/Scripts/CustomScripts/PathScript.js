Url =
function () { }
Url.prototype = {
    _relativeRoot: '<%= ResolveUrl("~/") %>',
    // create an extension method called "resolve"
    resolve: function (relative) {
        var resolved = relative;
        if (relative.charAt(0) == '~')
            resolved = this._relativeRoot + relative.substring(2);
        return resolved;
    }
}
$Url = new Url();