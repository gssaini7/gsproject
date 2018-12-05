
private Genericrepository<ImageGalleryModel> _Ta_ImageGalleryModel;
public Genericrepository<ImageGalleryModel> ImageGalleryRepository
{
	get
	{
		if (this._Ta_ImageGalleryModel == null) this._Ta_ImageGalleryModel = new Genericrepository<ImageGalleryModel>(_context);
			return _Ta_ImageGalleryModel;
	}
}

