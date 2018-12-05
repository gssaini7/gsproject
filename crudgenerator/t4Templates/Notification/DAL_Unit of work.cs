
private Genericrepository<NotificationModel> _Ta_NotificationModel;
public Genericrepository<NotificationModel> NotificationRepository
{
	get
	{
		if (this._Ta_NotificationModel == null) this._Ta_NotificationModel = new Genericrepository<NotificationModel>(_context);
			return _Ta_NotificationModel;
	}
}

