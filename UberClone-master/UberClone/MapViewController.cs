using Foundation;
using System;
using UIKit;
using MapKit;
using CoreLocation;
using CoreGraphics;
using System.Collections.Generic;

namespace UberClone
{
    public partial class MapViewController : UIViewController
    {
        private MapViewDelegate mapDelegate;
        private MKMapView mapView;
        private UISearchController searchController;
        public MapViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            setUpMapView(); 
            setUpSearchView();
            //create button
            UIButton button = UIButton.FromType(UIButtonType.RoundedRect);
            button.SetTitle("CALL UBER", UIControlState.Normal);
            button.Frame = new CGRect(mapView.Frame.Width / 2 - 90, mapView.Frame.Height / 2 + 350, mapView.Frame.Width / 2 - 10, 40);
            button.BackgroundColor = UIColor.Black;
            button.SetTitleColor(UIColor.Gray, UIControlState.Disabled);
            button.SetTitleColor(UIColor.White, UIControlState.Normal);
            button.TouchUpInside += (object sender, System.EventArgs e) =>
            {
                var errorAlertController = UIAlertController.Create("Error", "Cannot call! This is a demo app", UIAlertControllerStyle.Alert);
                errorAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                PresentViewController(errorAlertController, true, null);
            };
            View.Add(button);
        }
        public void setUpMapView()
        {
            // set the map view 
            mapView = new MKMapView(UIScreen.MainScreen.Bounds);
            mapView.MapType = MKMapType.Standard;
            mapView.ShowsUserLocation = true;
            View.AddSubview(mapView);
            // Add annotations
            var userCoordinate = new CLLocationCoordinate2D(-35.3160, 149.1070);
            var currentUserLocation = new MKPointAnnotation() { Title = "Your Location", Coordinate = userCoordinate };
            var driverUserLocation = new DriverAnnotation(("Driver's location"), new CLLocationCoordinate2D(-35.3169, 149.1075));
            mapView.AddAnnotation(currentUserLocation);
            mapView.AddAnnotation(driverUserLocation);
            // set the map delegate
            mapDelegate = new MapViewDelegate();
            mapView.Delegate = mapDelegate;
            // Add map center 
            const double lat = -35.3160;
            const double lon = 149.1070;
            var mapCenter = new CLLocationCoordinate2D(lat, lon);
            var mapRegion = MKCoordinateRegion.FromDistance(mapCenter, 100, 100);
            mapView.CenterCoordinate = mapCenter;
            mapView.Region = mapRegion;
        }

        public void setUpSearchView()
        {
            // add search
            var searchResultsController = new SearchViewController(mapView);

            var searchUpdater = new SearchResultsUpdator();
            searchUpdater.UpdateSearchResults += searchResultsController.UpdateSearchResults;

            // add the search controller
            searchController = new UISearchController(searchResultsController);
            searchController.SearchResultsUpdater = searchUpdater;

            searchController.SearchBar.SizeToFit();
            searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            searchController.SearchBar.Placeholder = "Enter a search query";

            searchController.HidesNavigationBarDuringPresentation = false;
            NavigationItem.TitleView = searchController.SearchBar;
            DefinesPresentationContext = true;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (searchController != null)
            {
                searchController.Dispose();
                searchController = null;
            }

            if (mapDelegate != null)
            {
                mapDelegate.Dispose();
                mapDelegate = null;
            }
        }
    }
}