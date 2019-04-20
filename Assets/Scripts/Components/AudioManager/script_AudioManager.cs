using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRYSTAL;

namespace IceFalls {

    public class script_AudioManager : CRYSTAL_Script {

        // Static

        public static script_AudioManager Instance {
            get {
                return GO.Find("AudioManager").GetComponent<script_AudioManager>();
            }
        }

        // Public

        public List<AudioSource> BGM_Sources = new List<AudioSource>();
        public List<string> BGM_SourceNames = new List<string>();
        public float BackgroundMusicVolume;

        // --

        public List<AudioSource> SE_Sources = new List<AudioSource>();
        public List<string> SE_SourceNames = new List<string>();
        public float SoundEffectVolume;

        // Public Methods


        // Background Music

        public void PlayBackgroundMusic(string _SourceName) {

            this.StopBackgroundMusic();

            script_AudioSource source = this.GetBackgroundMusicSource(_SourceName);
            if (source != null) {
                source.UpdateVolume(this.BackgroundMusicVolume);
                source.Source.Play();
            }
        }

        public void StopBackgroundMusic() {
            this.BGM_Sources.ForEach((AudioSource source) => {
                source.Stop();
            });
        }

        public void UpdateBackgroundMusicVolume(string _SourceName) {
            script_AudioSource source = this.GetBackgroundMusicSource(_SourceName);
            source.UpdateVolume(this.BackgroundMusicVolume);
        }

        public script_AudioSource GetBackgroundMusicSource(string _SourceName) {
            return this.GetBackgroundMusicSource(this.BGM_SourceNames.IndexOf(_SourceName));
        }

        public script_AudioSource GetBackgroundMusicSource(int _Index) {
            if (_Index >= 0 && _Index < this.BGM_Sources.Count) {
                return this.BGM_Sources[_Index].GetComponent<script_AudioSource>();
            }
            return null;
        }

        // Sound Effects

        public void PlaySoundEffect(string _SourceName) {

            script_AudioSource source = this.GetBackgroundMusicSource(_SourceName);
            if (source != null) {
                source.Source.Stop();
                source.UpdateVolume(this.BackgroundMusicVolume);
                source.Source.Play();
            }
        }

        public void UpdateSoundEffectVolume(string _SourceName) {
            script_AudioSource source = this.GetSoundEffectSource(_SourceName);
            if (source != null) {
                source.UpdateVolume(SoundEffectVolume);
            }
        }

        public script_AudioSource GetSoundEffectSource(string _SourceName) {
            return this.GetSoundEffectSource(this.SE_SourceNames.IndexOf(_SourceName));
        }

        public script_AudioSource GetSoundEffectSource(int _Index) {
            if (_Index >= 0 && _Index < this.SE_Sources.Count) {
                return this.SE_Sources[_Index].GetComponent<script_AudioSource>();
            }
            return null;
        }
    }
}