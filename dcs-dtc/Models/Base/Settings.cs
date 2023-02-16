﻿using Newtonsoft.Json;

namespace DTC.Models.Base
{
	public static class Settings
	{
		private class SettingsData
		{
			public int TCPSendPort;
			public int UDPReceivePort;
			public int CommandDelayMs;
			public string UploadHotKey;
			public int AltitudeAsElev;
		}

		private static SettingsData currentSettings;

		public static string UploadHotKey
		{
			get
			{
				LoadSettings();
				return currentSettings.UploadHotKey;
			}
			set
			{
				currentSettings.UploadHotKey = value;
				SaveSettings();
			}
		}

		public static int AltitudeAsElev
		{
			get
			{
				LoadSettings();
				return currentSettings.AltitudeAsElev;
			}
			set
			{
				currentSettings.AltitudeAsElev = value;
				SaveSettings();
			}
		}
		public static int TCPSendPort
		{
			get
			{
				LoadSettings();
				return currentSettings.TCPSendPort;
			}
			set
			{
				currentSettings.TCPSendPort = value;
				SaveSettings();
			}
		}

		public static int UDPReceivePort
		{
			get
			{
				LoadSettings();
				return currentSettings.UDPReceivePort;
			}
			set
			{
				currentSettings.UDPReceivePort = value;
				SaveSettings();
			}
		}

		public static int CommandDelayMs
		{
			get
			{
				LoadSettings();
				return currentSettings.CommandDelayMs;
			}
			set
			{
				currentSettings.CommandDelayMs = value;
				SaveSettings();
			}
		}

		private static void SaveSettings()
		{
			var json = JsonConvert.SerializeObject(currentSettings);
			FileStorage.PersistSettingsFile(json);
		}

		private static void LoadSettings()
		{
			if (currentSettings != null) {
				return;
			}

			var json = FileStorage.LoadSettingsFile();
			SettingsData obj = null;
			try
			{
				obj = JsonConvert.DeserializeObject<SettingsData>(json);
			}
			catch {}

			if (obj == null)
			{
				obj = new SettingsData();
			}

			if (obj.TCPSendPort == 0)
			{
				obj.TCPSendPort = 43001;
			}
			if (obj.UDPReceivePort == 0)
			{
				obj.UDPReceivePort = 43000;
			}
			if (obj.CommandDelayMs == 0)
			{
				obj.CommandDelayMs = 50;
			}
			if (obj.UploadHotKey == "")
			{
				obj.UploadHotKey = "RCtrl+Back";
			}
			if (obj.AltitudeAsElev == 0)
			{
				obj.AltitudeAsElev = 0;
			}
			currentSettings = obj;
		}
	}
}
