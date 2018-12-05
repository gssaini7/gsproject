
private Genericrepository<NotificationScheduleModel> _Ta_NotificationScheduleModel;
public Genericrepository<NotificationScheduleModel> NotificationScheduleRepository
{
	get
	{
		if (this._Ta_NotificationScheduleModel == null) this._Ta_NotificationScheduleModel = new Genericrepository<NotificationScheduleModel>(_context);
			return _Ta_NotificationScheduleModel;
	}
}

