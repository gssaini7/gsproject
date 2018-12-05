
private Genericrepository<TCSSRelModel> _Ta_TCSSRelModel;
public Genericrepository<TCSSRelModel> TCSSRelRepository
{
	get
	{
		if (this._Ta_TCSSRelModel == null) this._Ta_TCSSRelModel = new Genericrepository<TCSSRelModel>(_context);
			return _Ta_TCSSRelModel;
	}
}

