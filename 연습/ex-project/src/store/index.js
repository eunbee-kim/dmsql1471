import Vue from "vue";
import Vuex from "vuex";
import router from "../router/index"

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    userinfo: null,
    allUsers: [
      {
        id: 1,
        name: "김은비",
        email: "dmsql1471@nate.com",
        password: "1234",
      },
      { id: 2, name: "dmsql", email: "dmsql@nate.com", password: "1234" },
    ],
    isLogin: false,
    isLoginError: false
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
    },
  },

  //비즈니스 로직
  actions: {
    //로그인 시도
    login({ state, commit }, loginObj) {
      let selectedUser = null;
      state.allUsers.forEach((user) => {
        if (user.email === loginObj.email) selectedUser = user;
      });

      if (selectedUser === null || selectedUser.password !== loginObj.password)
        commit("loginError")
      else {
        commit("loginSuccess", selectedUser)
        router.push({ name: 'mypage' })
      }
    },
    logout({ commit }) {
      commit("logout")
      router.push({ name: 'Home' })
    }
  }
});
