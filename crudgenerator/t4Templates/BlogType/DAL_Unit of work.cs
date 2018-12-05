
private Genericrepository<BlogTypeModel> _Ta_BlogTypeModel;
public Genericrepository<BlogTypeModel> BlogTypeRepository
{
	get
	{
		if (this._Ta_BlogTypeModel == null) this._Ta_BlogTypeModel = new Genericrepository<BlogTypeModel>(_context);
			return _Ta_BlogTypeModel;
	}
}

