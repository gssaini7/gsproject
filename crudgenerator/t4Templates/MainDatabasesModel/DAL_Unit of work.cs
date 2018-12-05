
private Genericrepository<MainDatabasesModel> _Ta_MainDatabasesModel;
public Genericrepository<MainDatabasesModel> MainDatabasesRepository
{
	get
	{
		if (this._Ta_MainDatabasesModel == null) this._Ta_MainDatabasesModel = new Genericrepository<MainDatabasesModel>(_context);
			return _Ta_MainDatabasesModel;
	}
}

