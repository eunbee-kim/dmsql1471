import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import router from "../router/index"

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    userinfo: null,
    allUsers: [
      // {
      //   id: 1,
      //   name: "김은비",
      //   email: "dmsql1471@nate.com",
      //   password: "1234",
      // },
      // { id: 2, name: "dmsql", email: "dmsql@nate.com", password: "1234" },
    ],
    isLogin: false,
    isLoginError: false,
  },

  //state 값을 변화
  mutations: {
    //로그인이 성공했을 때
    loginSuccess(state, payload) {
      state.isLogin = true
      state.isLoginError = false
      state.userinfo = payload
    },
    //로그인이 실패했을 때
    loginError(state) {
      state.isLogin = false
      state.isLoginError = true
    },
    logout(state) {
      state.isLogin = false
      state.isLoginError = false
      state.userinfo = null
      localStorage.removeItem("access_token");
    },
  },

  //비즈니스 로직
  actions: {
    //로그인 시도
    login({ dispatch }, loginObj) {
      //로그인 -> 토큰 반환
      axios
        .post("https://reqres.in/api/login", loginObj)
        .then((res) => {
          //요청 성공 시 토큰이 반환됨
          let token = res.data.token
          //토큰을 로컬스토리지에 저장
          localStorage.setItem("access_token", token)
          dispatch("getMemberInfo")

        })
        .catch(() => {
          alert("이메일과 비밀번호를 확인해주세요.")
          router.push({ name: 'Home' })
        });
    },

    //   let selectedUser = null;
    //   state.allUsers.forEach((user) => {
    //     if (user.email === loginObj.email) selectedUser = user;
    //   });

    //   if (selectedUser === null || selectedUser.password !== loginObj.password)
    //     commit("loginError")
    //   else {
    //     commit("loginSuccess", selectedUser)
    //     router.push({ name: 'mypage' })
    //   }

    logout({ commit }) {
      commit("logout")
      alert("로그아웃 되었습니다")
      window.location.reload()
    },
    getMemberInfo({ commit }) {
      //로컬스토리지에 있는 토큰을 불러온다
      let token = localStorage.getItem("access_token")
      if (token !== null) {

        //토큰을 헤더에 포함시켜서 유저정보를 요청
        //토큰 -> 멤버 정보를 반환
        let config = {
          headers: {
            "access-token": token
          }
        }
        //새로 고침 -> 토큰만 가지고 멤버정보를 요청

        axios
          .get("https://reqres.in/api/users/2", config)
          .then(response => {
            let userinfo = {
              id: response.data.data.id,
              first_name: response.data.data.first_name,
              last_name: response.data.data.last_name,
              avatar: response.data.data.avatar
            }
            commit("loginSuccess", userinfo)
            router.push({ name: 'Home' })
          })
          .catch(() => {
            alert("이메일과 비밀번호를 확인해주세요.")
          })
      }
    }
  }
});
