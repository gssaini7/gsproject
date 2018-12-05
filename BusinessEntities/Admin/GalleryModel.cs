
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace R.BusinessEntities
{
    public class AlbumModel : EntityCommonModel
    {
        #region properties

        public Guid AlbumModelid { get; set; }
        public string AlbumName { get; set; }
        #endregion
    }

    public class ImageGalleryModel : EntityCommonModel
    {
        #region properties

        public Guid ImageGalleryModelid { get; set; }
        public string ImageName { get; set; }
        public Guid AlbumModelid { get; set; }
        [ForeignKey("AlbumModelid")]
        public AlbumModel RelatedAlbum { get; set; }
        #endregion
    }

}
