 public class NotificationService : iNotificationService
    {
        private readonly cUnitOfWork _unitOfWork;

        public NotificationService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
        }

        public Guid Create(NotificationModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.SetDatabase(dbn);

                _unitOfWork.NotificationRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.NotificationModelid;
            }
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotificationModel> GetAll(string dbn)
        {
                _unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.NotificationRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public NotificationModel GetById(Guid tid, string dbn)
        {
                _unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.NotificationRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(NotificationModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                _unitOfWork.SetDatabase(dbn);

                    _unitOfWork.NotificationRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, NotificationModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }
    }

