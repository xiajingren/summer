﻿using System;
using System.Security.Cryptography;

namespace Summer.Shared.Utils
{
    public class CommonHelper
    {
        public static CommonHelper Instance { get; } = new CommonHelper();

        private CommonHelper()
        {
        }

        public string GenerateRandomNumber(int len = 32)
        {
            var randomNumber = new byte[len];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}