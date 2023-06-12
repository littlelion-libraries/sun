using System;

namespace Suns
{
    public static class SunUtils
    {
        public const float Obliquity = 23.45f;

        public static SunData GetSun(double latitude, double longitude, DateTime dateTime)
        {
            var latRad = latitude * Math.PI / 180;
            var lonRad = longitude * Math.PI / 180;
            var dayOfYear = dateTime.DayOfYear;
            var decimalTime = dateTime.Hour / 24 + dateTime.Minute / (24 * 60) + dateTime.Second / (24 * 60 * 60);
            var sunDeclination = Obliquity * Math.PI / 180 * Math.Sin(2 * Math.PI / 365 * (dayOfYear - 81));
            var sunHourAngle = 2 * Math.PI * (decimalTime - 0.5);
            var sinSunAltitude = Math.Sin(latRad) * Math.Sin(sunDeclination) +
                                 Math.Cos(latRad) * Math.Cos(sunDeclination) * Math.Cos(sunHourAngle);
            var sunAltitude = Math.Asin(sinSunAltitude);
            var cosAzimuth = (Math.Sin(latRad) * Math.Cos(sunAltitude) - Math.Sin(sunDeclination)) /
                             (Math.Cos(latRad) * Math.Sin(sunAltitude));
            var sinAzimuth = Math.Cos(sunAltitude) * Math.Sin(sunHourAngle) / Math.Cos(sunAltitude);
            var sunAzimuth = Math.Acos((float)cosAzimuth);
            if (sinAzimuth < 0)
            {
                sunAzimuth = 2 * Math.PI - sunAzimuth;
            }

            return new SunData
            {
                Altitude = sunAltitude,
                Azimuth = sunAzimuth
            };
        }
    }
}