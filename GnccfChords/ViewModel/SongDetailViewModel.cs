using CommunityToolkit.Mvvm.ComponentModel;
using GnccfChords.Helpers;
using GnccfChords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GnccfChords.ViewModel
{
    public class SongDetailViewModel: ObservableObject
    {
        private HttpClient _httpClient;
        private string _songName;
        public string SongName
        {
            get
            {
                return _songName;
            }
            set
            {
                _songName = value;
                OnPropertyChanged();
            }
        }
        private string _songKey;
        public string SongKey
        {
            get
            {
                return _songKey;
            }
            set
            {
                _songKey = value;
                OnPropertyChanged();
            }
        }
        private string _songChords;
        public string SongChords
        {
            get
            {
                return _songChords;
            }
            set
            {
                _songChords = value;
                OnPropertyChanged();
            }
        }

        public SongDetailViewModel(Guid songId)
        {
            _httpClient = new HttpClient();
            Task.Run(() => GetChordsBySongId(songId)).Wait();
        }

        private async Task GetChordsBySongId(Guid songId)
        {
            try
            {
                var gnccfApi = APIEndpoints.GnccfChordsAPI;

                var endpoint = $"{gnccfApi}/ChordParts?songId={songId}";
                var byteArray = Encoding.ASCII.GetBytes($"{APIEndpoints.UserName}:{APIEndpoints.Password}");
                var base64String = Convert.ToBase64String(byteArray);

                // Add the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64String);

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<SongChords>();

                    SongName = content.SongName;
                    SongKey = content.ChordKey;
                    SongChords = content.Chords;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
    }
}
