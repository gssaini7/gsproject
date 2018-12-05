
private Genericrepository<SiteModel> _Ta_SiteModel;
public Genericrepository<SiteModel> SiteRepository
{
	get
	{
		if (this._Ta_SiteModel == null) this._Ta_SiteModel = new Genericrepository<SiteModel>(_context);
			return _Ta_SiteModel;
	}
}

