
private Genericrepository<MenuItemModel> _Ta_MenuItemModel;
public Genericrepository<MenuItemModel> MenuItemRepository
{
	get
	{
		if (this._Ta_MenuItemModel == null) this._Ta_MenuItemModel = new Genericrepository<MenuItemModel>(_context);
			return _Ta_MenuItemModel;
	}
}

