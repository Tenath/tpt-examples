package com.tpt.mynotes;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.time.format.DateTimeFormatter;
import java.util.List;

public class NotesAdapter extends RecyclerView.Adapter<NotesAdapter.ViewHolder>
{
    // ViewHolder - контейнер для ссылок на элементы интерфейса в
    // layout конкретной записи в RecyclerView
    public class ViewHolder extends RecyclerView.ViewHolder
            implements View.OnClickListener
    {
        public TextView tvTitle;
        public TextView tvDateModified;
        public TextView tvContent;

        // Конструктор принимает ссылку на конкретный item, и
        // получает от него ссылки на элементы интерфейса, которые
        // необходимо изменять
        public ViewHolder(@NonNull View itemView) {
            super(itemView);

            tvTitle = itemView.findViewById(R.id.tvTitle);
            tvDateModified = itemView.findViewById(R.id.tvDateModified);
            tvContent = itemView.findViewById(R.id.tvContent);

            itemView.setOnClickListener(this);
        }

        @Override
        public void onClick(View v) {
            int position = getAdapterPosition();
            if(position != RecyclerView.NO_POSITION)
            {
                Note n = notes.get(position);
                Toast.makeText(ctx, n.Title, Toast.LENGTH_SHORT).show();

                // Задание:
                // 1. сделать новый Activity (название типа "NoteEdit")
                //    в нём сделать LinearLayout (вертикальный), в котором:
                //    TextEdit для заголовка записки
                //    TextView для отображения времени изменения
                //    TextEdit (multiline) для содержимого
                // 2. При нажатии на элемент в списке открывать этот Activity
                //    (пока без привязки/передачи данных)
                // Д/З:
                // 3. Реализовать добавление/удаление
                // 4. При внесении изменений пересохранять в файл

                // NB! При внесении изменений в список объектов, используемых
                // RecyclerView, его необходимо уведомлять через методы
                // NotesAdapter.notifyItemChanged(int position)
                // NotesAdapter.notifyInserted(int position)
                // NotesAdapter.notifyRemoved(int position)
                MainActivity ma = NotesApp.getInstance().getMainActivity();
                Intent intent = new Intent(ma, NoteEdit.class);
                ma.startActivity(intent);

            }
        }
    }

    private List<Note> notes;
    private Context ctx;
    public NotesAdapter(List<Note> lstnotes) { notes = lstnotes; }

    // Здесь создаём View (элемент интерфейса) для одной записи в RecyclerList
    // И создаём для него ViewHolder
    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        ctx = parent.getContext();

        LayoutInflater inflater = LayoutInflater.from(ctx);
        View noteView = inflater.inflate(R.layout.item_note,parent,false);
        return new ViewHolder(noteView);
    }

    // Наполняем View (элемент интерфейса) данными из модели (объект типа Note)
    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
        // Получаем из общего списка записок элемент с указанным индесом position
        Note n = notes.get(position);

        holder.tvTitle.setText(n.Title);
        DateTimeFormatter fmt = DateTimeFormatter.ofPattern("dd.MM.yyyy HH:mm");
        String date = n.DateModified.format(fmt);
        holder.tvDateModified.setText(date);
        holder.tvContent.setText(n.Content);
    }

    @Override
    public int getItemCount() {
        return notes.size();
    }
}
