using CommunityToolkit.Mvvm.ComponentModel;
using GnccfChords.Helpers;
using GnccfChords.Models;
using GnccfChords.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GnccfChords.ViewModel
{
    public class AddChordsViewModel: ObservableObject
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
        private string _artist;
        public string Artist
        {
            get => _artist;
            set
            {
                if (_artist != value)
                {
                    _artist = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _introChord;
        public string IntroChord
        {
            get
            {
                return _introChord;
            }
            set
            {
                _introChord = value;
                OnPropertyChanged();
            }
        }
        private string _preChorusChords;
        public string PreChorusChords
        {
            get
            {
                return _preChorusChords;
            }
            set
            {
                _preChorusChords = value;
                OnPropertyChanged();
            }
        }
        private string _chorusChord;
        public string ChorusChord
        {
            get
            {
                return _chorusChord;
            }
            set
            {
                _chorusChord = value;
                OnPropertyChanged();
            }
        }
        private string _bridgeChord;
        public string BridgeChord
        {
            get
            {
                return _bridgeChord;
            }
            set
            {
                _bridgeChord = value;
                OnPropertyChanged();
            }
        }
        private char _chordKey;
        public char ChordKey
        {
            get
            {
                return _chordKey;
            }
            set
            {
                _chordKey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveSongAndChord {  get; set; }
        private INavigation _navigation;
        public AddChordsViewModel(INavigation navigation) 
        {
            _navigation = navigation;
            _httpClient = new HttpClient();
            SaveSongAndChord = new Command(async () =>
            {
                await SaveSongAndChordAsync();
            });
        }

        private async Task SaveSongAndChordAsync()
        {
            try
            {
                var gnccfApi = APIEndpoints.GnccfChordsAPI;

                var songEndpoint = $"{gnccfApi}/Songs";
                var chordEndpoint = $"{gnccfApi}/ChordParts";
                var song = new Song
                {
                    SongName = SongName,
                    Artist = Artist,
                };

                JsonContent payload = JsonContent.Create(song);
                var response = await _httpClient.PostAsync(songEndpoint, payload);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<Guid>();
                    var chord = new Chords
                    {
                        SongId = content,
                        ChordKey = ChordKey,
                        IntroChords = IntroChord,
                        PreChorusChords = PreChorusChords,
                        ChorusChords = ChorusChord,
                        BridgeChords = BridgeChord,
                    };

                    JsonContent chordPayload = JsonContent.Create(chord);
                    var chordResponse = await _httpClient.PostAsync(chordEndpoint, chordPayload);
                    if (chordResponse.IsSuccessStatusCode) 
                    {
                        await _navigation.PopAsync(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
