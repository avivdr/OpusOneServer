using OpusOneServerBL.MusicModels;

namespace OpusOneServer.DTO
{
    public static class DataConverter
    {
        public static OmniSearchDTO ToOmniSearchDTO(this OmniSearchResult omniSearchResult)
        {
            if (omniSearchResult.Status == null || 
                omniSearchResult.Status?.Success != "true")
                return null;

            OmniSearchDTO omniSearchDTO = new()
            {
                Next = omniSearchResult.Next,
                Composers = new(),
                Works = new(),
            }; 

            foreach (var item in omniSearchResult.Items)
            {
                if (item == null || item.Composer == null) continue;

                if (item.Work == null)
                {
                    omniSearchDTO.Composers.Add(item.Composer);
                    continue;
                }

                item.Work.Composer = item.Composer;
                omniSearchDTO.Works.Add(item.Work);
            }

            return omniSearchDTO;
        }
    }
}
