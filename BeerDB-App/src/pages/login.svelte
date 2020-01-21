<script>
    import Header from './slots/header.svelte'
    import axios from 'axios'

    let username = "";
    let password = "";
    let user = {loggedIn: false}

    function login() {
      axios({
        method: 'post',
        url: "http://localhost:50698/api/auth/login",
        data: {
          username: username,
          password: password
        }
      })
      .then(function(response){
          console.log(response.data);
          document.cookie = "username=" + response.data;
          user.loggedIn = !user.loggedIn;
          console.log(user.loggedIn)
          document.cookie = "loggedIn=" + user.loggedIn;
          axios({
            method: 'post',
            url: "http://localhost:50698/api/auth/token",
            data: {
              username: username,
              password: password
            }
          })
          .then(function(response){
            console.log(response.data.token)
            document.cookie = "token=" + response.data.token;
            window.location='./'
          })
          .catch(err => console.log(err))
      })
      .catch(err => document.getElementById("pswNotFound").innerHTML = "Username or password not found")
    }

</script>

<main>
    <Header headerMessage={"Login"} logoImage="beer.png"></Header>

    <form on:submit|preventDefault={login}>

        <div class="loginForm">
          <div>
            <label for="uname"><b>Username</b></label>
            <input bind:value={username} type="text" placeholder="Enter Username" name="uname" required>
          </div>
          <div>
            <label for="psw"><b>Password</b></label>
            <input bind:value={password} type="password" placeholder="Enter Password" name="psw" required>
          </div>
          
          <button type="submit">Login</button>
          <p id="pswNotFound"></p>
          <a href="/register">Don't have an account yet? Register here</a>
        </div>
    </form>
</main>

<style>
  .loginForm {
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    align-content: flex-start;
  }

  input[type=text], input[type=password] {
    width: 500px;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
  }

  button[type=submit]{
    width: 500px;
    padding: 12px 20px;
    margin: 8px 0;
    background-color: green;
    color: white;
    border: none;
    cursor: pointer;
  }

  button[type=submit]:hover {
    opacity: 0.8;
  }

  #pswNotFound {
    color: red;
  }
</style>