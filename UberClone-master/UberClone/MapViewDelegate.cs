using System;
using MapKit;
using UIKit; 
namespace UberClone
{
    public class MapViewDelegate : MKMapViewDelegate
    {
        private string userID = "User";
        private string driverID = "Driver"; 
        UIImage car = new UIImage("UberImage.png");
        public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            if (overlay is MKPolyline)
            {
                var route = (MKPolyline)overlay;
                var renderer = new MKPolylineRenderer(route) { StrokeColor = UIColor.Blue };
                return renderer;
            }
            return null;
        }
        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView pinView = null;

            if (annotation is MKUserLocation)
                return pinView;

            if (annotation is DriverAnnotation)
            {
                pinView = mapView.DequeueReusableAnnotation(driverID) ?? new MKAnnotationView(annotation, driverID);
                pinView.CanShowCallout = true;
                pinView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
                pinView.Draggable = true;
                pinView.Image = ResizeImage(UIImage.FromFile("UberImage.png"), 50, 50);
                pinView.LeftCalloutAccessoryView = new UIImageView(ResizeImage(UIImage.FromFile("DriverImage.png"), 50, 50)); 
            } else
            {
                var annotationView = mapView.DequeueReusableAnnotation(userID) as MKPinAnnotationView ?? new MKPinAnnotationView(annotation, userID);
                annotationView.PinTintColor = UIColor.Red;
                annotationView.CanShowCallout = true;
                pinView = annotationView;
            }
            return pinView;
        }

        // resize the image (without trying to maintain aspect ratio)
        public UIImage ResizeImage(UIImage sourceImage, float width, float height)
        {
            UIGraphics.BeginImageContext(new CoreGraphics.CGSize(width, height));
            sourceImage.Draw(new CoreGraphics.CGRect(0,0, width, height)); 
            var resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }
        // Add Polyline
    }
}
