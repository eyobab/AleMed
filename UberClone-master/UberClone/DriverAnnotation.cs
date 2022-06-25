using System;
using CoreLocation;
using MapKit;
namespace UberClone
{
    public class DriverAnnotation : MKAnnotation
    {
        private CLLocationCoordinate2D coordinate;
        private readonly string title;
        public DriverAnnotation(string title, CLLocationCoordinate2D coordinate)
        {
            this.coordinate = coordinate;
            this.title = title;
        }
        public override string Title => title;
        public override CLLocationCoordinate2D Coordinate => coordinate;
    }
}
