 public class FeedbackService : iFeedbackService
    {
        private readonly cUnitOfWork _unitOfWork;
        private commonclass clsobj;


        public FeedbackService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
			this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(FeedbackModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);

                _unitOfWork.FeedbackRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.FeedbackModelid;
            }
        }

        public bool Delete(Guid tid)
        {
             var success = false;

            using (var scope = new TransactionScope())
            {
               _unitOfWork.FeedbackRepository.Delete(tentity);
                _unitOfWork.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }

        public IEnumerable<FeedbackModel> GetAll()
        {
                clsobj.SetDataBase(dbn);

            var results = _unitOfWork.FeedbackRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public FeedbackModel GetById(Guid tid)
        {
                clsobj.SetDataBase(dbn);

            var result = _unitOfWork.FeedbackRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(FeedbackModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.FeedbackRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, FeedbackModel tentity)
        {
            throw new NotImplementedException();
        }
    }

