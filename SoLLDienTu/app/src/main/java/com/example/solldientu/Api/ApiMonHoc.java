package com.example.solldientu.Api;

import com.example.solldientu.object.MonHoc;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;
import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface ApiMonHoc {
    String url = "https://solldientu-yg3.conveyor.cloud/api/MonHoc/";

    OkHttpClient client = new OkHttpClient.Builder()
            .connectTimeout(30, TimeUnit.SECONDS)
            .writeTimeout(60, TimeUnit.SECONDS)
            .readTimeout(60, TimeUnit.SECONDS)
            .build();

    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .setLenient()
            .create();

    ApiMonHoc apiService = new Retrofit.Builder().baseUrl(url)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(client)
            .build()
            .create(ApiMonHoc.class);

    // lấy môn học theo kì
    @GET("get-kimonhoc/{kihoc}")
    Call<List<MonHoc>> getMonHoc(@Path("kihoc") int kh);

    @GET("get-tongtccacmon")
    Call<Integer> getTongTCMH();

    @GET("get-sotcmondahoc/{kihoc}/{masv}")
    Call<Integer> getSoTCdaHocMH(@Path("kihoc") int ki, @Path("masv") String ma);
}
