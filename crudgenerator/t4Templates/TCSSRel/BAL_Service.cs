 public class TCSSRelService : iTCSSRelService
    {
        private readonly cUnitOfWork _unitOfWork;
        private commonclass clsobj;


        public TCSSRelService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
			this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(TCSSRelModel tentity)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);

                _unitOfWork.TCSSRelRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.TCSSRelModelid;
            }
        }

        public bool Delete(Guid tid)
        {
             var success = false;

            using (var scope = new TransactionScope())
            {
               _unitOfWork.TCSSRelRepository.Delete(tentity);
                _unitOfWork.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }

        public IEnumerable<TCSSRelModel> GetAll()
        {
                clsobj.SetDataBase(dbn);

            var results = _unitOfWork.TCSSRelRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public TCSSRelModel GetById(Guid tid)
        {
                clsobj.SetDataBase(dbn);

            var result = _unitOfWork.TCSSRelRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(TCSSRelModel tentity)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    _unitOfWork.TCSSRelRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, TCSSRelModel tentity)
        {
            throw new NotImplementedException();
        }
    }

