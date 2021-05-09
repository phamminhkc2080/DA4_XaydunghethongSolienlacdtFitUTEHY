package com.example.solldientu;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.solldientu.Adapter.SubjectsAdapter;
import com.example.solldientu.Api.ApiMonHoc;
import com.example.solldientu.object.MonHoc;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SubjectsActivity extends AppCompatActivity {
    ArrayList<Integer> arrSoki;
    ArrayAdapter adapterSoki;
    Spinner spinnerSoki;
    int pos;

    ArrayList<MonHoc> arr_mh;
    SubjectsAdapter subjectsAdapter;
    ListView lv_dsmh;

    TextView txt_TongSoTC, txt_SoTCDaHoc;

    String maSV="";

    Toolbar toolbar;
    ActionBar actionBar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_subjects_activity);
        Init();
        maSV=getIntent().getStringExtra("maSV");
        Event();
    }

    private void Event() {
        ApiMonHoc.apiService.getTongTCMH().enqueue(new Callback<Integer>() {
            @Override
            public void onResponse(Call<Integer> call, Response<Integer> response) {
                txt_TongSoTC.setText("/"+response.body());
            }

            @Override
            public void onFailure(Call<Integer> call, Throwable t) {

            }
        });

        spinnerSoki.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                pos = position;

                ApiMonHoc.apiService.getMonHoc(arrSoki.get(position)).enqueue(new Callback<List<MonHoc>>() {
                    @Override
                    public void onResponse(Call<List<MonHoc>> call, Response<List<MonHoc>> response) {
                        ArrayList<MonHoc> ds_mh1 = (ArrayList<MonHoc>) response.body();
                        arr_mh.clear();
                        if (ds_mh1.size() > 0) {
                            for (int i = 0; i < ds_mh1.size(); i++) {
                                arr_mh.add(ds_mh1.get(i));
                            }
                            subjectsAdapter.notifyDataSetChanged();
                        }
                    }

                    @Override
                    public void onFailure(Call<List<MonHoc>> call, Throwable t) {
                        Toast.makeText(SubjectsActivity.this, "Fail " + t.toString(), Toast.LENGTH_SHORT).show();
                    }
                });

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
        ApiMonHoc.apiService.getSoTCdaHocMH(arrSoki.get(pos), maSV).enqueue(new Callback<Integer>() {
            @Override
            public void onResponse(Call<Integer> call, Response<Integer> response) {
                txt_SoTCDaHoc.setText(response.body() + "");
            }

            @Override
            public void onFailure(Call<Integer> call, Throwable t) {

            }
        });
    }

    private void Init() {
        toolbar=findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        actionBar=getSupportActionBar();//Get actionbar
        actionBar.setTitle("Môn học");
        actionBar.setDisplayHomeAsUpEnabled(true);//Hiện nút bấm 3 gạch (Phải có cả đoạn code(drawerToggle) bên dưới)

        spinnerSoki = findViewById(R.id.sp_Ky);
        arrSoki = new ArrayList<>();
        arrSoki.add(1);
        arrSoki.add(2);
        arrSoki.add(3);
        arrSoki.add(4);
        arrSoki.add(5);
        arrSoki.add(6);
        arrSoki.add(7);
        arrSoki.add(8);
        adapterSoki = new ArrayAdapter(SubjectsActivity.this, android.R.layout.simple_spinner_item, arrSoki);
        adapterSoki.setDropDownViewResource(android.R.layout.simple_list_item_single_choice);
        spinnerSoki.setAdapter(adapterSoki);

        lv_dsmh = (ListView) findViewById(R.id.lvmonHoc);
        arr_mh = new ArrayList<>();
        subjectsAdapter = new SubjectsAdapter(SubjectsActivity.this, R.layout.subject_adapter, arr_mh);
        lv_dsmh.setAdapter(subjectsAdapter);

        txt_TongSoTC = (TextView) findViewById(R.id.txt_tongsotc);
        txt_SoTCDaHoc = (TextView) findViewById(R.id.txt_sotcdahoc);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()){
            case android.R.id.home:
                finish();
        }
        return super.onOptionsItemSelected(item);
    }
}