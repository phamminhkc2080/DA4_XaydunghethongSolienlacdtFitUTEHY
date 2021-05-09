package com.example.solldientu.Fragment;

import android.app.ProgressDialog;
import android.os.Bundle;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.widget.Toolbar;
import androidx.fragment.app.Fragment;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.solldientu.Adapter.ResultAdapter;
import com.example.solldientu.Adapter.SubjectsAdapter;
import com.example.solldientu.Api.ApiKetQua;
import com.example.solldientu.R;
import com.example.solldientu.SubjectsActivity;
import com.example.solldientu.object.KetQua;
import com.example.solldientu.object.MonHoc;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link ResultFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class ResultFragment extends Fragment {

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    public ResultFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment ResultFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static ResultFragment newInstance(String param1, String param2) {
        ResultFragment fragment = new ResultFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    View view;

    ArrayList<Integer> arrSoki;
    ArrayAdapter adapterSoki;
    Spinner spinnerSoki;
    int pos;

    ArrayList<KetQua> arr_kq;
    ResultAdapter resultAdapter;
    ListView lv_kq;

    TextView txt_TBCHocTap, txt_XLHocTap;

    String maSv="";
    ProgressDialog pd;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        view = inflater.inflate(R.layout.fragment_result, container, false);
        Init();
        maSv=getActivity().getIntent().getStringExtra("maSV");
        Events();
        return view;
    }

    private void Events() {

        spinnerSoki.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                LoadData(i);
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
    }

    private void LoadData(int i) {
        pd=new ProgressDialog(getContext());
        pd.setMessage("Đang tải dữ liệu...");
        pd.show();

        ApiKetQua.apiService.get_KetQua(maSv, arrSoki.get(i)).enqueue(new Callback<List<KetQua>>() {
            @Override
            public void onResponse(Call<List<KetQua>> call, Response<List<KetQua>> response) {
                arr_kq= (ArrayList<KetQua>) response.body();
                resultAdapter = new ResultAdapter(getContext(), R.layout.ketqua_adapter, arr_kq);
                lv_kq.setAdapter(resultAdapter);

                pd.dismiss();
            }

            @Override
            public void onFailure(Call<List<KetQua>> call, Throwable t) {

            }
        });
    }

    private void Init() {
        spinnerSoki = view.findViewById(R.id.sp_Ky);
        arrSoki = new ArrayList<>();
        arrSoki.add(1);
        arrSoki.add(2);
        arrSoki.add(3);
        arrSoki.add(4);
        arrSoki.add(5);
        arrSoki.add(6);
        arrSoki.add(7);
        arrSoki.add(8);
        adapterSoki = new ArrayAdapter(getContext(), android.R.layout.simple_spinner_item, arrSoki);
        adapterSoki.setDropDownViewResource(android.R.layout.simple_list_item_single_choice);
        spinnerSoki.setAdapter(adapterSoki);

        lv_kq = view.findViewById(R.id.lvMHKQ);
        arr_kq = new ArrayList<>();

        txt_TBCHocTap = view.findViewById(R.id.txt_TBCHocTap);
        txt_XLHocTap = view.findViewById(R.id.txt_XLHocTap);
    }
}