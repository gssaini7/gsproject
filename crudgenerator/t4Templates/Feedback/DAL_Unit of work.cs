
private Genericrepository<FeedbackModel> _Ta_FeedbackModel;
public Genericrepository<FeedbackModel> FeedbackRepository
{
	get
	{
		if (this._Ta_FeedbackModel == null) this._Ta_FeedbackModel = new Genericrepository<FeedbackModel>(_context);
			return _Ta_FeedbackModel;
	}
}

