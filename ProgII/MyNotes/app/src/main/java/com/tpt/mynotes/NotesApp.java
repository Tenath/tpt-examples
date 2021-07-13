package com.tpt.mynotes;

import android.content.Context;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

// Класс "Приложение"
// Тоже реализуем по схеме Singleton
public class NotesApp {
    //private static Context ctx = null;
    private static NotesApp app = null;
    private static MainActivity mainActivity = null;

    private ArrayList<Note> notes;
    public Note currentNote = null;

    private NotesApp()
    {
        //context = ctx;
        notes = new ArrayList<>();

        // Test data
        //Note.SeedData(notes);
        try {
            //Note.WriteFile(notes, "notes.txt", ctx);
            Context ctx = getContext();
            // Получаем метаданные по файлу
            File f = new File(ctx.getFilesDir(),"notes.txt");
            // изучаем наличие файла и содержимого в нём
            if(!f.exists())
            {
                Note.SeedData(notes);
                Note.WriteFile(notes, "notes.txt", ctx);
            }

            long filesize = f.length();
            String path = f.getAbsolutePath();
            notes = Note.ReadFile("notes.txt", ctx);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static NotesApp getInstance()
    {
        if(app == null) app = new NotesApp();
        return app;
    }

    public static Context getContext() { return mainActivity.getApplicationContext(); }

    public ArrayList<Note> getNotes() { return notes; }

    public static MainActivity getMainActivity() { return mainActivity; }
    public static void setMainActivity(MainActivity ma) { mainActivity=ma; }
}
