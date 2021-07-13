package com.tpt.mynotes;

import android.content.Context;
import android.util.Log;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.nio.charset.StandardCharsets;
import java.sql.Array;
import java.sql.Timestamp;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

public class Note {
    public String Title;
    public String Content;
    public LocalDateTime DateCreated;
    public LocalDateTime DateModified;

    public String Serialize()
    {
        DateTimeFormatter fmt = DateTimeFormatter.ofPattern("dd.MM.yyyy-HH:mm");
        return String.format("%s,%s,%s,%s\n", Title, Content, fmt.format(DateCreated),
                fmt.format(DateModified));
    }

    public static Note Unserialize(String str)
    {
        String[] fields = str.split(",");

        Note n = new Note();

        n.Title = fields[0];
        n.Content = fields[1];
        DateTimeFormatter fmt = DateTimeFormatter.ofPattern("dd.MM.yyyy-HH:mm");
        n.DateCreated = LocalDateTime.parse(fields[2], fmt);
        n.DateModified = LocalDateTime.parse(fields[3], fmt);

        return n;
    }

    public static ArrayList<Note> ReadFile(String filename, Context ctx)
    {
        ArrayList<Note> lst = new ArrayList<>();
        // Здесь будет проводить десериализацию объектов из файла
        try (BufferedReader br = new BufferedReader(new
                InputStreamReader(ctx.openFileInput(filename),
                StandardCharsets.UTF_8)))
        {
            String line = br.readLine();
            while(line != null)
            {
                Note n = Note.Unserialize(line);
                lst.add(n);
                line = br.readLine();
            }

        }
        catch(Exception e)
        {

        }

        return lst;
    }

    public static void WriteFile(ArrayList<Note> what, String where, Context ctx)
            throws IOException
    {
        try(OutputStreamWriter os = new OutputStreamWriter(
                ctx.openFileOutput(where,Context.MODE_PRIVATE)))
        //try(FileOutputStream os = ctx.openFileOutput(where, Context.MODE_PRIVATE))
        {
            for (Note n : what)
            {
                String line = n.Serialize();
                //os.write(line.getBytes(StandardCharsets.UTF_8));
                os.write(line);
            }
        }
        catch(Exception e)
        {
            Log.println(1,"IO",e.getMessage().toString());
        }
    }

    public static void SeedData(ArrayList<Note> lst)
    {
        Note n = new Note();
        n.Title = "First note";
        n.Content = "This is the content of the first note.";
        n.DateCreated = LocalDateTime.now();
        n.DateModified = LocalDateTime.now();

        lst.add(n);

        n = new Note();
        n.Title = "Second note";
        n.Content = "This is the content of the second note.";
        n.DateCreated = LocalDateTime.now();
        n.DateModified = LocalDateTime.now();

        lst.add(n);

        n = new Note();
        n.Title = "Third note";
        n.Content = "This is the content of the third note.";
        n.DateCreated = LocalDateTime.now();
        n.DateModified = LocalDateTime.now();

        lst.add(n);
    }
}
