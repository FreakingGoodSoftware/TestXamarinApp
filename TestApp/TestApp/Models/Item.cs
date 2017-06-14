namespace TestApp.Models
{
    public class Item : BaseDataObject
    {
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string address = string.Empty;
        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        string rating = string.Empty;

        public string Rating
        {
            get { return rating; }
            set { SetProperty(ref rating, value); }
        }

        string image = string.Empty;

        public string Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }
    }
}
