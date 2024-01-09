using OpusOneServerBL.MusicModels;

namespace OpusOneServer.DTO
{
    public static class DataConverter
    {
        public static OmniSearchDTO ToOmniSearchDTO(this OmniSearchResult omniSearchResult)
        {
            if (omniSearchResult == null || omniSearchResult.Status == null)
                return null;

            OmniSearchDTO omniSearchDTO = new()
            {
                Next = omniSearchResult.Next,
                Composers = new(),
                Works = new(),
            };

            if (omniSearchResult.Items == null)
                return omniSearchDTO;

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
