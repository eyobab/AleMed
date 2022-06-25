using Foundation;
using System;
using UIKit;
using MapKit;
using CoreLocation;
using CoreGraphics;
using System.Collections.Generic;

namespace UberClone
{
    // Searching class
    public class SearchResultsUpdator : UISearchResultsUpdating
    {
        public event Action<string> UpdateSearchResults;

        public override void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            UpdateSearchResults?.Invoke(searchController.SearchBar.Text);
        }
    }

    public class SearchViewController : UITableViewController
    {
        private UIButton button; 
        private const string CellIdentifier = "mapItemCellId";
        private List<MKMapItem> items;

        private MKMapView map;

        public SearchViewController(MKMapView map)
        {
            this.map = map; 
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return items?.Count ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) ?? new UITableViewCell();
            cell.TextLabel.Text = items[indexPath.Row].Name;
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // add item to map
            var coordinate = items[indexPath.Row].Placemark.Location.Coordinate;
            map.AddAnnotations(new MKPointAnnotation
            {
                Coordinate = coordinate,
                Title = items[indexPath.Row].Name,
            });
            showDirection(new CLLocationCoordinate2D(-35.3160, 149.1070), coordinate);
            showDirection(new CLLocationCoordinate2D(-35.3169, 149.1075), new CLLocationCoordinate2D(-35.3160, 149.1070));
            createUsersDetails(map);
            map.SetCenterCoordinate(coordinate, true);
            DismissViewController(false, null);
        }
        public void createUsersDetails(MKMapView map)
        {
            // Create Driver's Detail
            UIView userDetails = new UIView();
            userDetails.BackgroundColor = null;
            userDetails.UserInteractionEnabled = true;
            userDetails.Frame = new CGRect(map.Frame.Width / 2 - 90, map.Frame.Height / 2 + 200, map.Frame.Width / 2 - 10, map.Frame.Height / 4 - 75);
            map.AddSubview(userDetails);
            UIImage driverImage = new UIImage("Car.png");
            UIImageView imageView = new UIImageView(driverImage);
            imageView.Frame = new CGRect(userDetails.Frame.Width / 4 + 15, 10, 75, 75);
            imageView.Layer.MasksToBounds = true;
            imageView.Layer.CornerRadius = 20;
            userDetails.Add(imageView);
            UILabel driverLabel = new UILabel();
            driverLabel.Frame = new CGRect(userDetails.Frame.Width / 4, 20, userDetails.Frame.Height, userDetails.Frame.Width);
            driverLabel.LineBreakMode = UILineBreakMode.WordWrap;
            driverLabel.Lines = 0;
            driverLabel.Font = UIFont.BoldSystemFontOfSize(16);
            driverLabel.Text = "Name: UberX  Price: 50$";
            userDetails.Add(driverLabel);
        }
        public void showDirection(CLLocationCoordinate2D userCoordinate, CLLocationCoordinate2D destinationCoordinate)
        {
            var req = new MKDirectionsRequest()
            {
                Source = new MKMapItem(new MKPlacemark(userCoordinate)),
                Destination = new MKMapItem(new MKPlacemark(destinationCoordinate)),
                TransportType = MKDirectionsTransportType.Automobile,
                RequestsAlternateRoutes = true
            };
            var dir = new MKDirections(req);
            dir.CalculateDirections((response, error) =>
            {
                if (error == null)
                {
                    //Add each Polyline from route to map as overlay
                    foreach (var route in response.Routes)
                    {
                        map.AddOverlay(route.Polyline);
                    }
                }
                else
                {
                    Console.WriteLine(error.LocalizedDescription);
                }
            });
        }

        public void UpdateSearchResults(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                // create search request
                var searchRequest = new MKLocalSearchRequest
                {
                    NaturalLanguageQuery = query,
                    Region = new MKCoordinateRegion(map.UserLocation.Coordinate, new MKCoordinateSpan(0.25, 0.25))
                };

                // perform search
                var localSearch = new MKLocalSearch(searchRequest);
                localSearch.Start((response, error) =>
                {
                    if (response != null && error == null)
                    {
                        items = new List<MKMapItem>(response.MapItems);
                        this.TableView.ReloadData();
                    }
                    else
                    {
                        Console.WriteLine($"local search error: {error?.LocalizedDescription ?? ""}");
                    }
                });
            }
        }
    }
}
