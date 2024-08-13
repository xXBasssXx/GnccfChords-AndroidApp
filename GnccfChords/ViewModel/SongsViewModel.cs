using CommunityToolkit.Mvvm.ComponentModel;
using GnccfChords.Helpers;
using GnccfChords.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GnccfChords.ViewModel
{
    public class SongsViewModel: ObservableObject
    {
        private HttpClient _httpClient;
        private ObservableCollection<Song> songs { get; set; }
        private bool isLoading { get; set; }
        private string searchText;

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                if(searchText.Length >= 3)
                {
                    Task.Run(() => GetSongs(searchText));
                }
                if (string.IsNullOrEmpty(searchText))
                {
                    Task.Run(() => GetSongs());
                }
            }
        }
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Song> SongsResult
        {
            get
            {
                return songs;
            }
            set
            {
                songs = value;
                OnPropertyChanged(nameof(SongsResult));
            }
        }
        private bool _isRefreshed;
        public bool IsRefreshed
        {
            get
            {
                return this._isRefreshed;
            }
            set
            {
                this._isRefreshed = value;
                OnPropertyChanged(nameof(IsRefreshed));
            }
        }
        public ICommand RefreshCommand { get; set; }
        public SongsViewModel() 
        {
            _httpClient = new HttpClient();
            Task.Run(() => GetSongs().Wait());

            RefreshCommand = new Command(async () =>
            {
                await GetSongs("");
            });
        }

        private async Task GetSongs(string search = "")
        {
            try
            {
                IsLoading = true;
                var gnccfApi = APIEndpoints.GnccfChordsAPI;

                var endpoint = $"{gnccfApi}/Songs?search={search}";

                var byteArray = Encoding.ASCII.GetBytes($"{APIEndpoints.UserName}:{APIEndpoints.Password}");
                var base64String = Convert.ToBase64String(byteArray);

                // Add the Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64String);

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Song>>();

                    SongsResult = content;
                    IsLoading = false;
                    IsRefreshed = false;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
