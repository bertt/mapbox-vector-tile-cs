namespace Mapbox.Vectors
{
    public class ScreenPixelToLatLon
    {
        public static Coordinate Convert(long x, long y, int zoomLevel)
        {
            /**
            var g = (y - this.zc[zoom]) / (-this.Cc[zoom]);
            var lon = (px[0] - this.zc[zoom]) / this.Bc[zoom];
            var lat = R2D * (2 * Math.atan(Math.exp(g)) - 0.5 * Math.PI);
            return [lon, lat];
             */
            return null;
        }
    }
}


/**
 * https://github.com/mapbox/mapbox-studio/blob/5ac2ead1e523b24c8b8ad8655babb66389166e87/ext/sphericalmercator.js
 * 
 * 
 * // Convert screen pixel value to lon lat
//
// - `px` {Array} `[x, y]` array of geographic coordinates.
// - `zoom` {Number} zoom level.
SphericalMercator.prototype.ll = function(px, zoom) {
    var g = (px[1] - this.zc[zoom]) / (-this.Cc[zoom]);
    var lon = (px[0] - this.zc[zoom]) / this.Bc[zoom];
    var lat = R2D * (2 * Math.atan(Math.exp(g)) - 0.5 * Math.PI);
    return [lon, lat];
};*/
