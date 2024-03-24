
const song = document.getElementById("song");
const playBtn = document.querySelector(".player-inner");
const nextBtn = document.querySelector(".play-next");
const prevBtn = document.querySelector(".play-back");
const durationTime = document.querySelector(".duration");
const remainingTime = document.querySelector(".remaining");
const rangeBar = document.querySelector(".range");
const musicName = document.querySelector(".music-name");
const musicSinger = document.querySelector(".music-singer");
const musicThumb = document.querySelector(".music-thumb");
const musicImage = document.querySelector(".music-thumb img");
const playRepeat = document.querySelector(".play-repeat")
const playRandomBtn = document.querySelector(".play-infi")
const musicImages = document.querySelectorAll(".music-image");
const rangevolume = document.getElementById('volume');




let isPlaying = true;
let indexSong = 0;
let isRepeat = false;
// const musiclist = ["焔は嗤う.mp3","idol-kanata.mp3","Speak-Out.mp3","ばかばっか.mp3"];


console.log(musics);

let timer;
let repeatCount = 0;
playRepeat.addEventListener("click", function () {
    if (isRepeat) {
        repeatCount = 1;
        isRepeat = false;
        playRepeat.removeAttribute("style");
    } else {
        repeatCount = 0;
        isRepeat = true;
        playRepeat.style.color = "#ffb86c";
    }
});

nextBtn.addEventListener("click", function () {
    // if(israndom){
    //     const randomIndex = Math.floor(Math.random() * tempMusics.length);
    //     indexSong = randomIndex;
    //     console.log(indexSong);
    //     init(indexSong);
    //     song.play()
    //     tempMusics.splice(indexSong, 1); // Remove the selected song from tempMusics
    //     musics.splice(indexSong, 1)

    // }else {

    changeSong(1);
    //   }
});

prevBtn.addEventListener("click", function () {
    changeSong(-1);
});

var israndom = false;
playRandomBtn.addEventListener("click", function () {
    if (israndom) {
        israndom = false;
        playRandomBtn.removeAttribute("style");
    } else {
        israndom = true;
        playRandomBtn.style.color = "#ffb86c";
        tempMusics = musics.slice(); // Create a copy of the original musics array
        console.log(tempMusics)



    }
});

//chức năng chuyển bài 
song.addEventListener("ended", handleEndedSong); // add event khi kết thúc 

function handleEndedSong() {
    repeatCount++;
    if (isRepeat && repeatCount === 1) {
        // handle repeat song
        isPlaying = true;

        playPause();
    } else if (israndom) {
        var index = Math.floor(Math.random() * 1000) % musics.length;
        console.log(index, musics[id]);
        changeSong(index)
    } else {
        changeSong(1);
    }

}
// chức chuyển nhạc
function changeSong(dir) {
    if (dir === 1) {
        // next song
        indexSong++;
        if (indexSong >= musics.length) {
            indexSong = 0;
        }
        isPlaying = true;
    } else if (dir === -1) {
        // prev song
        indexSong--;
        if (indexSong < 0) {
            indexSong = musics.length - 1;
        }
        isPlaying = true;
    }
    init(indexSong);
    playPause();

}


// play music and pause when click
playBtn.addEventListener("click", playPause);
function playPause() {
    if (isPlaying) {
        musicThumb.classList.add("is-playing");
        song.play();
        playBtn.innerHTML = '<ion-icon name="pause-outline"></ion-icon>';
        isPlaying = false;
        timer = setInterval(displayTimer, 500);// set timer khi chạy
    } else {
        musicThumb.classList.remove("is-playing");
        song.pause();
        playBtn.innerHTML = '<ion-icon name="play-outline"></ion-icon>';
        isPlaying = true;
        clearInterval(timer);
    }
}




//hiện timer của nhạc 
function displayTimer() {
    const { duration, currentTime } = song;
    rangeBar.max = duration;
    rangeBar.value = currentTime;
    remainingTime.textContent = formatTimer(currentTime);

    if (!duration) {
        durationTime.textContent = "00:00";

    } else {
        durationTime.textContent = formatTimer(duration - currentTime);

        var value = (currentTime / duration) * 100;
        rangeBar.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #fff ' + value + '%, white 100%)';

    }


}
// hiển thị thời gian của nhạc
function formatTimer(number) {
    const minutes = Math.floor(number / 60);
    const seconds = Math.floor(number - minutes * 60);
    return `${minutes < 10 ? '0' + minutes : minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
}


// chức năng tua nhạc 
rangeBar.addEventListener("change", handleChangeBar);
function handleChangeBar() {
    song.currentTime = rangeBar.value;
}

// tang giam am luong
rangevolume.addEventListener("input", handleChangeVolume);
function handleChangeVolume() {
    song.volume = rangevolume.value / 10;

}

musicImages.forEach(function (image) {
    image.addEventListener("click", function () {

            // Get the song ID from the data attribute
            const songId = parseInt(image.dataset.songId);

            // Find the index of the song with the corresponding ID
            const index = musics.findIndex(song => song.id === songId);
            console.log(songId);

            // If a song with the corresponding ID is found, change to that song

        
    });
});

function init(indexSong) {

    song.setAttribute("src", `./music/${musics[indexSong].file}`);
    musicImage.setAttribute("src",`./Imgae_Musics/${musics[indexSong].image}`);
    musicName.textContent = musics[indexSong].name;
    musicSinger.textContent = musics[indexSong].idol.name;

}

displayTimer();
init(indexSong);

document.getElementById("volume").oninput = function () {
    var value = (this.value - this.min) / (this.max - this.min) * 100
    this.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #fff ' + value + '%, white 100%)'
};


document.getElementById("range").oninput = function () {
    var value = (this.value - this.min) / (this.max - this.min) * 100
    this.style.background = 'linear-gradient(to right, #82CFD0 0%, #82CFD0 ' + value + '%, #fff ' + value + '%, white 100%)'
};