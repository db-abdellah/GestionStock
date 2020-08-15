﻿using GestionStock.Models.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Handlers
{
    public class Log
    {
       
        public static void logWriter(IHostEnvironment env, Utilisateur util)
        {
            try
            {
                String filePath = env.ContentRootPath + @"/logFile.txt";
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine( util.nom + " ||" + util.prenom + " || " + util.fonction + " || " + DateTime.Now);

                   
                }


               
                
             
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

     

        public static List<Utilisateur> fileToList(IHostEnvironment env)
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            String filePath = env.ContentRootPath + @"/logFile.txt";
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Utilisateur util = new Utilisateur();
                string[] col = line.Split("||");
                util.nom = col[0];
                util.prenom = col[1];
                util.fonction = col[2];
                util.date = col[3];
                utilisateurs.Add(util);
                   

            }
            return utilisateurs;
        }
    }
}
