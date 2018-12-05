 public class SettingsService : iSettingsService
    {
        private readonly cUnitOfWork _unitOfWork;
		 private commonclass clsobj;

        public SettingsService(cUnitOfWork unitOfWork) 
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);

        }

        public Guid Create(SettingsModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
               clsobj.SetDataBase(dbn);

                _unitOfWork.SettingsRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.SettingsModelid;
            }
        }

        public bool Delete(Guid tid, string dbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SettingsModel> GetAll(string dbn)
        {
               clsobj.SetDataBase(dbn);

            var results = _unitOfWork.SettingsRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public SettingsModel GetById(Guid tid, string dbn)
        {
              clsobj.SetDataBase(dbn);

            var result = _unitOfWork.SettingsRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        
        public bool Update(SettingsModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                 clsobj.SetDataBase(dbn);

                    _unitOfWork.SettingsRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, SettingsModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

		public bool Delete(SettingsModel tentity, string dbn)
        {
            var success = false;

            using (var scope = new TransactionScope())
            {
                _unitOfWork.SettingsRepository.Delete(tentity);
                _unitOfWork.Save();
                scope.Complete();
                success = true;
            }
            return success;
        }
    }

