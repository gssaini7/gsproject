
private Genericrepository<SettingsModel> _Ta_SettingsModel;
public Genericrepository<SettingsModel> SettingsRepository
{
	get
	{
		if (this._Ta_SettingsModel == null) this._Ta_SettingsModel = new Genericrepository<SettingsModel>(_context);
			return _Ta_SettingsModel;
	}
}

