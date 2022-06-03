package com.tpt.mynotes;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;

public class NoteEdit extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_note_edit);
    }

    @Override
    public void onBackPressed()
    {
        super.onBackPressed();
    }
}