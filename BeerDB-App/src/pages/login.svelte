<script>
    import Header from './slots/header.svelte'
    import axios from 'axios'

    let username = "";
    let password = "";

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
          alert('welcome ' + username)
      })
      .catch(err => document.getElementById("demo").innerHTML = "Username or password not found")
    }
</script>

<main>
    <Header></Header>
    <h2>Login</h2>

    <form on:submit|preventDefault={login}>

        <div class="container">
            <label for="uname"><b>Username</b></label>
            <input bind:value={username} type="text" placeholder="Enter Username" name="uname" required>

            <label for="psw"><b>Password</b></label>
            <input bind:value={password} type="password" placeholder="Enter Password" name="psw" required>
            
            <label>
            <button type="submit">Login</button>
            <p id="demo"></p>
        </div>
    </form>
</main>

<style>

</style>