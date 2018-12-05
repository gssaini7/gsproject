
private Genericrepository<LayoutModel> _Ta_LayoutModel;
public Genericrepository<LayoutModel> LayoutRepository
{
	get
	{
		if (this._Ta_LayoutModel == null) this._Ta_LayoutModel = new Genericrepository<LayoutModel>(_context);
			return _Ta_LayoutModel;
	}
}

