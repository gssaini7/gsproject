
private Genericrepository<ClassModel> _Ta_ClassModel;
public Genericrepository<ClassModel> ClassRepository
{
	get
	{
		if (this._Ta_ClassModel == null) this._Ta_ClassModel = new Genericrepository<ClassModel>(_context);
			return _Ta_ClassModel;
	}
}

