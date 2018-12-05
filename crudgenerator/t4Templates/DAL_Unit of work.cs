
private Genericrepository<MenuModel> _Ta_MenuModel;
public Genericrepository<MenuModel> MenuRepository
{
	get
	{
		if (this._Ta_MenuModel == null) this._Ta_MenuModel = new Genericrepository<MenuModel>(_context);
			return _Ta_MenuModel;
	}
}

