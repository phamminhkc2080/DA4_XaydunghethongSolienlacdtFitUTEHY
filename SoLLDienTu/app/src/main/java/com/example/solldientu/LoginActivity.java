package com.example.solldientu;

import androidx.appcompat.app.AppCompatActivity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.text.InputType;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import com.example.solldientu.Api.ApiTaiKhoan;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {

    ImageView visible_img;
    EditText account_edt, pass_edt;
    Button login_btn;
    int is_visible = 1;
    String maSV = "";

    ProgressDialog pd;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        Init();
        Events();
    }

    private void Events() {
        visible_img.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (is_visible == 1) {
                    is_visible = 0;
                    visible_img.setImageResource(R.drawable.ic_visibility_20);
                    pass_edt.setInputType(InputType.TYPE_CLASS_TEXT);
                } else {
                    is_visible = 1;
                    visible_img.setImageResource(R.drawable.ic_visibility_off_20);
                    pass_edt.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);
                }
            }
        });
        login_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Login();
            }
        });
    }

    private void Login() {
        pd=new ProgressDialog(this);
        pd.setMessage("Đang đăng nhập...");
        pd.show();

        if (account_edt.getText().toString().equals("") || pass_edt.getText().toString().equals("")) {
            pd.dismiss();
            Toast.makeText(LoginActivity.this, "Tài khoản và mật khẩu không được để trống !", Toast.LENGTH_SHORT).show();
        } else {
            ApiTaiKhoan.apiService.getMa(account_edt.getText().toString(), pass_edt.getText().toString()).enqueue(new Callback<String>() {
                @Override
                public void onResponse(Call<String> call, Response<String> response) {
                    pd.dismiss();
                    if (response.isSuccessful()) {
                        maSV = response.body();
                        Toast.makeText(LoginActivity.this, "Đăng nhập thành công !", Toast.LENGTH_SHORT).show();

                        Intent it = new Intent(LoginActivity.this, MainActivity.class);
                        it.putExtra("maSV", maSV);
                        startActivity(it);
                    } else {
                        Toast.makeText(LoginActivity.this, "Tài khoản hoặc mật khẩu không chính xác !", Toast.LENGTH_SHORT).show();
                    }
                }

                @Override
                public void onFailure(Call<String> call, Throwable t) {
                    pd.dismiss();
                    Toast.makeText(LoginActivity.this, "Tài khoản hoặc mật khẩu không chính xác!", Toast.LENGTH_SHORT).show();
                }
            });
        }
    }

    private void Init() {
        visible_img = findViewById(R.id.visible_img);
        account_edt = findViewById(R.id.account_edt);
        pass_edt = findViewById(R.id.pass_edt);
        login_btn = findViewById(R.id.login_btn);

        account_edt = (EditText) findViewById(R.id.account_edt);
        pass_edt = (EditText) findViewById(R.id.pass_edt);

    }
}