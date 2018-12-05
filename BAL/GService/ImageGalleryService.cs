using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using R.DAL.UnitOfWork;
using R.BusinessEntities;
using System.Transactions;
using DAL;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Net;

namespace R.BAL
{
    public class ImageGalleryService : iImageGalleryService
    {
        private readonly cUnitOfWork _unitOfWork;

        private commonclass clsobj;

        public ImageGalleryService(cUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.clsobj = new commonclass(this._unitOfWork);
        }

        public Guid Create(ImageGalleryModel tentity, string dbn)
        {
            using (var scope = new TransactionScope())
            {
                clsobj.SetDataBase(dbn);
                //_unitOfWork.SetDatabase(dbn);

                _unitOfWork.ImageGalleryRepository.Insert(tentity);
                _unitOfWork.Save();
                scope.Complete();
                return tentity.ImageGalleryModelid;
            }
        }

        public bool Delete(ImageGalleryModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid tid, string dbn)
        {
            var success = false;
            if (tid != null)
            {
                using (var scope = new TransactionScope())
                {
                    //clsobj.SetDataBase(dbn);
                    //_unitOfWork.SetDatabase(dbn);

                    var result = _unitOfWork.ImageGalleryRepository.GetByID(tid);
                    if (result != null)
                    {
                        _unitOfWork.ImageGalleryRepository.Delete(result);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<ImageGalleryModel> GetAll(string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.ImageGalleryRepository.GetAll();
            if (results.Any())
            {
                return results;
            }
            return null;
        }

        public IEnumerable<ImageGalleryModel> GetAllByAlbumId(Guid albumid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var results = _unitOfWork.ImageGalleryRepository.GetMany(i => i.AlbumModelid == albumid);
            if (results != null)
            {
                if (results.Any())
                {
                    return results;
                }
            }
            return null;
        }

        public ImageGalleryModel GetById(Guid tid, string dbn)
        {
            clsobj.SetDataBase(dbn);
            //_unitOfWork.SetDatabase(dbn);

            var result = _unitOfWork.ImageGalleryRepository.GetByID(tid);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public bool Update(ImageGalleryModel tentity, string dbn)
        {
            var success = false;
            if (tentity != null)
            {
                using (var scope = new TransactionScope())
                {
                    clsobj.SetDataBase(dbn);
                    //_unitOfWork.SetDatabase(dbn);

                    _unitOfWork.ImageGalleryRepository.Update(tentity);
                    _unitOfWork.Save();
                    scope.Complete();
                    success = true;
                }
            }

            return success;
        }

        public bool Update(Guid tid, ImageGalleryModel tentity, string dbn)
        {
            throw new NotImplementedException();
        }
    }


}
