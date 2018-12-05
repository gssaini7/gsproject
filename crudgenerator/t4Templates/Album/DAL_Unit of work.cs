
private Genericrepository<AlbumModel> _Ta_AlbumModel;
public Genericrepository<AlbumModel> AlbumRepository
{
	get
	{
		if (this._Ta_AlbumModel == null) this._Ta_AlbumModel = new Genericrepository<AlbumModel>(_context);
			return _Ta_AlbumModel;
	}
}

