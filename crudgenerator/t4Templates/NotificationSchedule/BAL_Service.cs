 public class NotificationScheduleService : iNotificationScheduleService
    {
        private readonly cUnitOfWork _unitOfWork;

        public NotificationScheduleService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(NotificationScheduleModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.SetDatabase(dbn);

                _unitOfWork.NotificationScheduleRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.NotificationScheduleModelid;
            }
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotificationScheduleModel> GetAll(string dbn)
        {
                _unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.NotificationScheduleRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public NotificationScheduleModel GetById(Guid tid, string dbn)
        {
                _unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.NotificationScheduleRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(NotificationScheduleModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                _unitOfWork.SetDatabase(dbn);

                    _unitOfWork.NotificationScheduleRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, NotificationScheduleModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }
    }

